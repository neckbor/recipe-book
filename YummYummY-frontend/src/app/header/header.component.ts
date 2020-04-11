import { Component, OnInit } from '@angular/core';
import {faBars} from '@fortawesome/free-solid-svg-icons';
import {faTimes} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  faBars = faBars;
  faTimes = faTimes;
  constructor() { }

  ngOnInit() {
  }
  private adaptiveHeader() {
    document.getElementById('menu-toggle').classList.toggle('active');
    document.getElementById('nav').classList.toggle('active');
  }

}
