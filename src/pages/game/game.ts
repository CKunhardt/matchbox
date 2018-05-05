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

  private startTime: number;
  private endTime: number;

  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    public authProvider: AuthProvider
  ) {
    this.gameStarted = false;
    this.buttonPos = 0;
  }

  findMatch(): void {
    this.tapCount = 30;
    this.buttonPos = (this.tapCount % 6) + 1;
    this.gameStarted = true;
    this.startTime = Date.now();
  }

  advance(): void {
    if(this.tapCount > 0){
      this.tapCount--;
      this.buttonPos = (this.tapCount % 6) + 1;
    }
    if(this.tapCount == 0){
      this.endTime = Date.now();
      this.buttonPos = 0;
      this.resultTime = Math.floor((this.endTime - this.startTime))/1000;
      let self = this;
      this.authProvider.updateUserLastTime(this.resultTime).then(function(){
        self.navCtrl.setRoot('results');
      })
    }
  }

  ionViewWillLeave() {
    this.tapCount = null;
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad GamePage');
  }

}
