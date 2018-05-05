import { Component } from '@angular/core';
import { Platform } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import firebase from 'firebase';

import { HomePage } from '../pages/home/home';
@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  rootPage:any;

  

  constructor(platform: Platform, statusBar: StatusBar, splashScreen: SplashScreen) {
    platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      statusBar.styleDefault();
      splashScreen.hide();

      firebase.initializeApp({
        apiKey: "AIzaSyB-lzxLhImbleqHWnbhDhQdbQSTo7_vErw",
        authDomain: "matchbox-201520.firebaseapp.com",
        databaseURL: "https://matchbox-201520.firebaseio.com",
        projectId: "matchbox-201520",
        storageBucket: "matchbox-201520.appspot.com",
        messagingSenderId: "996723655229"
      });

      const unsubscribe = firebase.auth().onAuthStateChanged(user => {
        if(!user){
          this.rootPage = 'login';
          unsubscribe();
        } else {
          this.rootPage = HomePage;
          unsubscribe();
        }
      });
      
    });
  }
}

