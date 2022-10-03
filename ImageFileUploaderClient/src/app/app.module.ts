import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { UploadImageComponent } from './upload-image/upload-image.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SafeurlPipe } from './pipe/safeurl.pipe';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    UploadImageComponent,
    SafeurlPipe
  ],
  imports: [
    BrowserModule, HttpClientModule, NgbModule, FormsModule,
    RouterModule.forRoot([
      { path: '', component: UploadImageComponent, pathMatch: 'full' },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
