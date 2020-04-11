import { Component, OnInit } from '@angular/core';
import {ShareService} from '../services/shareService';

@Component({
  selector: 'app-all-recipes',
  templateUrl: './all-recipes.component.html',
  styleUrls: ['./all-recipes.component.scss']
})
export class AllRecipesComponent implements OnInit {

  constructor(private shareRecipes: ShareService) {}

  ngOnInit() {
  }

}
