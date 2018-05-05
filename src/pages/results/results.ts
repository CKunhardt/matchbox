import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { AuthProvider } from '../../providers/auth/auth';
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

  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    public authProvider: AuthProvider
    ) {
      this.matchID = this.navParams.get("matchID");
      this.playerNum = this.navParams.get("playerNum");
    }

  goHome(): void {
    this.navCtrl.setRoot(HomePage);
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad ResultsPage');
    this.authProvider.getCurrentUser().then(user => this.currentUser = user);
    this.authProvider.getMatch(this.matchID).then(match => this.lastMatch = match);
  }

}
