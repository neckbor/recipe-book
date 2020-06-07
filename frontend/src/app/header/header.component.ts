import { Component, OnInit } from '@angular/core';
import {faBars} from '@fortawesome/free-solid-svg-icons';
import {faTimes} from '@fortawesome/free-solid-svg-icons';
import {User} from '../models/User';
import {CookieService} from 'ngx-cookie-service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  faBars = faBars;
  faTimes = faTimes;
  login: string;
  constructor(private cookie: CookieService) {
    this.login = cookie.get('login');
  }

  ngOnInit() {
  }
  private adaptiveHeader() {
    document.getElementById('menu-toggle').classList.toggle('active');
    document.getElementById('nav').classList.toggle('active');
  }
  private exit() {
    this.cookie.delete('login');
    this.cookie.delete('access_token');
    this.cookie.delete('role');
  }

}
