import { Component, OnInit } from '@angular/core';
import {CookieService} from "ngx-cookie-service";
import {Router} from "@angular/router";
import {RecipeService} from "../services/recipeService";
import {Ingredient} from "../models/Ingredient";
import {Nationality} from "../models/Nationality";

@Component({
  selector: 'app-all-nationalities',
  templateUrl: './all-nationalities.component.html',
  styleUrls: ['./all-nationalities.component.scss']
})
export class AllNationalitiesComponent implements OnInit {

  constructor(private cookie: CookieService, private router: Router, private http: RecipeService) { }

  nationalities;
  nameNationality = '';

  ngOnInit() {
    if (this.cookie.get('role') != 'admin') {
      this.router.navigate(['/']);
    } else {
      this.http.getAllNationalities(this.cookie.get('access_token')).subscribe( data => {
          this.nationalities = data.body;
        },
        err => console.log(err));
    }
  }
  public deleteNationality(id) {
    this.http.deleteNationality(id, this.cookie.get('access_token')).subscribe( data => {
        console.log('Успешно удален');
        this.http.getAllNationalities(this.cookie.get('access_token')).subscribe( data => {
            this.nationalities = data.body;
          },
          err => {
            console.log(err);
          });
      },
      err => {
        console.log(err);
        if(err.status === 500) {
          alert('Эта национальность используется в рецепте. Его нельзя удалить. Сначала удалите рецепт.')
        }})
  }

  public updateNationality(ingredient) {
    this.http.updateNationality(ingredient, this.cookie.get('access_token')).subscribe( data => {
        console.log('Умпешно изменен');
        this.http.getAllNationalities(this.cookie.get('access_token')).subscribe( data => {
            this.nationalities = data.body;
          },
          err => {
            console.log(err);
          },);
      },
      err => console.log(err))
  }

  public addNationality() {
    if (this.nameNationality == ''){
      alert('национальность не должна быть пустой')
    } else{
      const nationality = new Nationality();
      nationality.name = this.nameNationality;
      this.http.addNationality(nationality, this.cookie.get('access_token')).subscribe( data => {
          console.log('Успешно добавлен');
          this.http.getAllNationalities(this.cookie.get('access_token')).subscribe( data => {
              this.nationalities = data.body;
            },
            err => {
              console.log(err);
            });
        },
        err => console.log(err));
    }
  }
}
