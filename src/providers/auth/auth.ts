import { Injectable } from '@angular/core';
import { IUser } from './../../interfaces/user';

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

  resetPassword(email: string): Promise<void> {
    return firebase.auth().sendPasswordResetEmail(email);
  }

  logoutUser() : Promise<void> {
    return firebase.auth().signOut();
  }

}
