import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { AuthProvider } from '../../providers/auth/auth';
import { ResultsProvider } from './../../providers/results/results';
import { IUser } from './../../interfaces/user';
import { IMatch } from './../../interfaces/match';

import { HomePage } from '../home/home';

/**
 * Generated class for the ResultsPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage({
  name: 'results'
})
@Component({
  selector: 'page-results',
  templateUrl: 'results.html',
})
export class ResultsPage {

  public currentUser : IUser;
  public matchID: string;
  public lastMatch: IMatch;
  public playerNum: string;

  public winner: IUser;
  public loser: IUser;

  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    public authProvider: AuthProvider,
    public resultsProvider: ResultsProvider
    ) {
      this.matchID = this.navParams.get("matchID");
      this.playerNum = this.navParams.get("playerNum");

      this.authProvider.getMatch(this.matchID).then(match => {
        this.lastMatch = match;
        if(match.Winner != "TBD"){
          var self = this;
          this.resultsProvider.postMatchData(match).then(results => {
            self.winner = results[0];
            self.loser = results[1];
            self.authProvider.updateUserTSR(self.winner).then(_ =>{
              self.authProvider.updateUserTSR(self.loser).then(_ => {
                self.authProvider.getCurrentUser().then(user => self.currentUser = user);
              });
            });
          });
        } else {
          this.authProvider.getCurrentUser().then(user => this.currentUser = user);
        }
      });
    }

  goHome(): void {
    this.navCtrl.setRoot(HomePage);
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad ResultsPage');
  }

}
