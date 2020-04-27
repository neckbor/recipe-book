import { Component, OnInit } from '@angular/core';
import {User} from '../models/User';
import {RecipeService} from '../services/recipeService';
import {Router} from '@angular/router';
import {error} from 'util';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor(private http: RecipeService, private router: Router) {}
  user: User = new User();
  repeatPassword: string;
  receivedAndwer: Response;
  ngOnInit() {
  }

  private registration() {
    console.log(this.user.email);
    this.http.registerUser(this.user).subscribe(
      (data) => {
        console.log(data);
        this.router.navigate(['/']);
      }, err => console.log(err)
    );
  }

}
