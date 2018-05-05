import { Component } from '@angular/core';
import { AuthProvider } from '../../providers/auth/auth';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

/**
 * Generated class for the GamePage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage({
  name: 'game'
})
@Component({
  selector: 'page-game',
  templateUrl: 'game.html',
})
export class GamePage {

  public tapCount: number;
  public resultTime: number;
  public gameStarted: boolean;
  public buttonPos: number;

  public opponentId: string;
  public matchID: string;

  private startTime: number;
  private endTime: number;

  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    public authProvider: AuthProvider
  ) {
    this.opponentId = this.navParams.get("opponentId");
    this.matchID = this.navParams.get('matchID');
    this.gameStarted = false;
    this.buttonPos = 0;
  }

  findMatch(): void {
    this.tapCount = 30;
    this.buttonPos = 1;
    this.gameStarted = true;
    this.startTime = Date.now();
  }

  advance(): void {
    if(this.tapCount > 0){
      this.tapCount--;
    }
    if(this.tapCount == 0){
      this.endTime = Date.now();
      this.buttonPos = 0;
      this.resultTime = Math.floor((this.endTime - this.startTime))/1000;
      let self = this;
      this.authProvider.updateUserLastTime(this.resultTime).then(function(){
        if(self.matchID != null){
          self.authProvider.updateMatch(self.resultTime, self.matchID).then(_ => {
            self.navCtrl.setRoot('results', {
              matchID: self.matchID,
              playerNum: "2"
            });
          });
        } else if(self.opponentId != null){
          self.authProvider.createMatch(self.resultTime, self.opponentId).then(matchID => {
            self.navCtrl.setRoot('results', {
              matchID: matchID,
              playerNum: "1"
            });
          });
        }
      });
    }
  }

  ionViewWillLeave() {
    this.tapCount = null;
  }
}
