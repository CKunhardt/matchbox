import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ViewMatchesPage } from './view-matches';

@NgModule({
  declarations: [
    ViewMatchesPage,
  ],
  imports: [
    IonicPageModule.forChild(ViewMatchesPage),
  ],
})
export class ViewMatchesPageModule {}
