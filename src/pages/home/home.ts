import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { AuthProvider } from '../../providers/auth/auth';
import { IUser } from './../../interfaces/user';


@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage { 
  
  public currentUser : IUser;

  constructor(
    public navCtrl: NavController,
    public authProvider: AuthProvider
  ) {}

  findMatch(): void {
    this.navCtrl.push('find-match');
  }

  viewMatches() : void {
    this.navCtrl.push('view-matches');
  }

  logout(): void {
    var self = this;
    this.authProvider.logoutUser().then(function(){
      self.navCtrl.setRoot('login')});
  }

  ionViewWillLoad(){
    this.authProvider.getCurrentUser().then(user => this.currentUser = user);
  }

  ionViewWillLeave(){
  }
}
