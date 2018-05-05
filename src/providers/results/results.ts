import { Http, Headers, RequestOptions} from '@angular/http';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import { Injectable } from '@angular/core';

import { AuthProvider } from '../../providers/auth/auth';
import { IMatch } from './../../interfaces/match';
import { IUser } from './../../interfaces/user';

/*
  Generated class for the ResultsProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class ResultsProvider {

  public data = {
    Winner: {},
    Loser: {}
  }

  constructor(public http: Http, public authProvider: AuthProvider) {

  }

  postMatchData(matchData: IMatch) : Promise<IUser[]> {
    
    return this.prepareMatchData(matchData).then(_ => {
      var dataString = JSON.stringify(this.data);

      let headers = new Headers({
        'Content-Type' : 'application/json'
      });
      let options = new RequestOptions({ headers: headers});
  
      console.log(dataString);
  
      return this.http.post('http://localhost:8081/match', dataString, options)
          .toPromise()
          .then((response) => {
            console.log("API Response: ", response.json());
            var results = [];
            results.push(response.json().Winner);
            results.push(response.json().Loser);
            return results;
          })
          .catch((error) => {
            console.error('API Error : ', error.status);
            console.error('API Error : ', JSON.stringify(error));
            return null;
          });
    });
  }

  prepareMatchData(matchData: IMatch) : Promise<void> {
    var self = this;
    if(matchData.Winner == matchData.Player1){
      return this.authProvider.getUser(matchData.Player1).then(user => {
        console.log(user);
        self.data.Winner = user;
      }).then(_ => {
        return this.authProvider.getUser(matchData.Player2).then(user => {
          console.log(user);
          self.data.Loser = user;
        })
      });
    } else if (matchData.Winner == matchData.Player2){
      return this.authProvider.getUser(matchData.Player2).then(user => {
        console.log(user);
        self.data.Winner = user;
      }).then(_ => {
        return this.authProvider.getUser(matchData.Player1).then(user => {
          console.log(user);
          self.data.Loser = user;
        })
      });
    } else {
      console.log("Incorrect match data");
      return Promise.reject("Incorrect match data");
    }
  }
}
