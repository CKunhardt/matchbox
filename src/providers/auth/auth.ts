import { Injectable } from '@angular/core';
import { IUser } from './../../interfaces/user';
import { IMatch } from "./../../interfaces/match";


import firebase from 'firebase';

@Injectable()
export class AuthProvider {
  constructor() { }

  loginUser(email: string, password: string) : Promise<any> {
    return firebase.auth().signInWithEmailAndPassword(email, password);
  }

  signupUser(email: string, password: string) : Promise<any> {
    return firebase
      .auth()
      .createUserWithEmailAndPassword(email, password)
      .then( newUser => {
        firebase
          .database()
          .ref('/userProfile')
          .child(newUser.uid)
          .set({email: email, lastTime: "TBD", mu: 25.000, sigma: 8.333, tsr: 0});
      });
  }

  getCurrentUserID() : string {
    return firebase.auth().currentUser.uid;
  }

  getCurrentUser() : Promise<IUser> {
    var user = firebase.auth().currentUser;
    return firebase
      .database()
      .ref('/userProfile/' + user.uid)
      .once('value')
      .then(snapshot => {
        return <IUser>{
          Id: user.uid,
          EmailAddress: snapshot.val().email,
          LastTime: snapshot.val().lastTime,
          BestTime: snapshot.val().bestTime,
          Mu: snapshot.val().mu,
          Sigma: snapshot.val().sigma,
          TSR: snapshot.val().tsr
        };
      });
  }

  updateUserLastTime(time: number) : Promise<void>{
    var user = firebase.auth().currentUser;
    var updates = {};
    updates['/userProfile/' + user.uid + '/' + 'lastTime'] = time.toString();
    return this.getCurrentUser().then(user => {
      if((user.BestTime == "TBD") || (time < Number(user.BestTime))){
        updates['/userProfile/' + user.Id + '/' + 'bestTime'] = time.toString();
      }
      return firebase
      .database()
      .ref()
      .update(updates);
    });
  }

  findMatchingUsers() : Promise<IUser[]>{
    var user = firebase.auth().currentUser;
    return firebase
      .database()
      .ref('/userProfile')
      .orderByChild('TSR')
      .once('value')
      .then(snapshot => {
        var users = [];
        snapshot.forEach(userChild => {
          console.log(userChild.key);
          if(userChild.key != user.uid){
            users.push(<IUser>{
              Id: userChild.key,
              EmailAddress: userChild.val().email,
              LastTime: userChild.val().lastTime,
              BestTime: userChild.val().bestTime,
              Mu: userChild.val().mu,
              Sigma: userChild.val().sigma,
              TSR: userChild.val().tsr
            });
          }
        });
        return users;
      });
  }

  createMatch(time: number, opponentId: string) : Promise<string> {
    var user = firebase.auth().currentUser;
    var timestamp = Date.now().toString();
    return firebase
      .database()
      .ref('/matches')
      .child(timestamp)
      .set({
        player1: user.uid,
        player1time: time.toString(),
        player2: opponentId,
        player2time: "TBD",
        winner: "TBD"
      }).then(_ => {
        return timestamp;
      });
  }

  updateMatch(time: number, matchNumber: string) : Promise<void> {
    return firebase
      .database()
      .ref('/matches/' + matchNumber)
      .once('value')
      .then(snapshot =>{
        var updates = {};
        updates['player2time'] = time;
        if(Number(snapshot.val().player1time) < time) {
          updates['winner'] = snapshot.val().player1;
        } else if(Number(snapshot.val().player1time) > time) {
          updates['winner'] = snapshot.val().player2;
        } else {
          updates['winner'] = "Draw";
        }
        return firebase
        .database()
        .ref('/matches/' + matchNumber + '/')
        .update(updates);
      });
  }

  getPlayer1Matches() : Promise<IMatch[]> {
    var user = firebase.auth().currentUser;
    return firebase
      .database()
      .ref('/matches')
      .orderByChild('player1')
      .equalTo(user.uid)
      .once('value')
      .then(snapshot => {
        var matches = [];
        snapshot.forEach(match => {
          matches.push(<IMatch>{
            MatchID: match.key,
            Player1: match.val().player1,
            Player1Time: match.val().player1time,
            Player2: match.val().player2,
            Player2Time: match.val().player2time,
            Winner: match.val().winner
          });
        });
        return matches;
      });
  }

  getPlayer2Matches() : Promise<IMatch[]> {
    var user = firebase.auth().currentUser;
    return firebase
      .database()
      .ref('/matches')
      .orderByChild('player2')
      .equalTo(user.uid)
      .once('value')
      .then(snapshot => {
        var matches = [];
        snapshot.forEach(match => {
          matches.push(<IMatch>{
            MatchID: match.key,
            Player1: match.val().player1,
            Player1Time: match.val().player1time,
            Player2: match.val().player2,
            Player2Time: match.val().player2time,
            Winner: match.val().winner
          });
        });
        return matches;
      });
  }

  getMatch(matchID: string) : Promise<IMatch> {
    return firebase
      .database()
      .ref('/matches')
      .child(matchID)
      .once('value')
      .then(snapshot => {
        return <IMatch>{
          MatchID: matchID,
          Player1: snapshot.val().player1,
          Player1Time: snapshot.val().player1time,
          Player2: snapshot.val().player2,
          Player2Time: snapshot.val().player2time,
          Winner: snapshot.val().winner
        };
      });
  }

  resetPassword(email: string): Promise<void> {
    return firebase.auth().sendPasswordResetEmail(email);
  }

  logoutUser() : Promise<void> {
    return firebase.auth().signOut();
  }

}
