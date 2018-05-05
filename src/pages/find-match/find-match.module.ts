import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { FindMatchPage } from './find-match';

@NgModule({
  declarations: [
    FindMatchPage,
  ],
  imports: [
    IonicPageModule.forChild(FindMatchPage),
  ],
})
export class FindMatchPageModule {}
