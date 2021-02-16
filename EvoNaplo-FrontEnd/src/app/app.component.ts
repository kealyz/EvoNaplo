import { Component, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { MediaMatcher } from '@angular/cdk/layout';
import { assertNotNull } from '@angular/compiler/src/output/output_ast';
import { isDefined } from '@angular/compiler/src/util';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnDestroy{
  title = 'EvoNaplo-FrontEnd';
  mobileQuery: MediaQueryList;
  isLoggedIn = this.LoggedIn();
  

  private _mobileQueryListener: () => void;
  constructor
    (
      private router: Router,
      changeDetectorRef: ChangeDetectorRef,
      media: MediaMatcher
    ) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }
  logout() {
    this.router.navigate(['/login']);
    localStorage.removeItem("id_token");
  }

  LoggedIn(): boolean {
    return isDefined(localStorage.getItem("id_token"));
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

}
