import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { AuthProvider } from '../../providers/auth/auth';
import { IMatch } from "./../../interfaces/match";

/**
 * Generated class for the ViewMatchesPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage({
  name: 'view-matches'
})
@Component({
  selector: 'page-view-matches',
  templateUrl: 'view-matches.html',
})
export class ViewMatchesPage {

  public player1Matches: IMatch[];
  public player2Matches: IMatch[];

  public userID: string;

  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    public authProvider: AuthProvider
  ) {
    this.userID = this.authProvider.getCurrentUserID();
    this.authProvider.getPlayer1Matches().then(matches => this.player1Matches = matches);
    this.authProvider.getPlayer2Matches().then(matches => this.player2Matches = matches);
  }

  playMatch(matchID: string){
    this.navCtrl.setRoot('game', {
      matchID: matchID
    });
  }

  ionViewDidLoad() {}

}
