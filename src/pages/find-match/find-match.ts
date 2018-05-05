import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { AuthProvider } from '../../providers/auth/auth';
import { IUser } from './../../interfaces/user';

/**
 * Generated class for the FindMatchPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage({
  name: 'find-match'
})
@Component({
  selector: 'page-find-match',
  templateUrl: 'find-match.html',
})
export class FindMatchPage {

  public users: IUser[];

  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    public authProvider: AuthProvider
    ) {}

  ionViewDidLoad() {
    this.authProvider.findMatchingUsers().then(users =>{
      this.users = users;
    });
  }

  startMatch(opponentId: string){
    this.navCtrl.setRoot('game', {
      opponentId: opponentId
    });
  }

}
