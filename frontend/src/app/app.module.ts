import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import {RouterModule, Routes} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {RecipeService} from './services/recipeService';
import {ShareService} from './services/shareService';
import {HttpClientModule} from '@angular/common/http';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import { AllRecipesComponent } from './all-recipes/all-recipes.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RecipeComponent } from './recipe/recipe.component';
import {MatStepperModule} from '@angular/material/stepper';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { RegistrationComponent } from './registration/registration.component';
import {CookieService} from 'ngx-cookie-service';
import { LoginComponent } from './login/login.component';
import { MyRecipesComponent } from './my-recipes/my-recipes.component';
import { ChangeRecipeComponent } from './change-recipe/change-recipe.component';
import {MatSelectModule} from "@angular/material/select";
import {MatInputModule} from "@angular/material/input";
import { AddRecipeComponent } from './add-recipe/add-recipe.component';

const appRoutes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'all-recipes', component: AllRecipesComponent},
  {path: 'recipe/:id', component: RecipeComponent},
  {path: 'registration', component: RegistrationComponent},
  {path: 'login', component: LoginComponent},
  {path: 'my-recipes', component: MyRecipesComponent},
  {path: 'change-recipe/:id', component: ChangeRecipeComponent},
  {path: 'add-recipe', component: AddRecipeComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    AllRecipesComponent,
    RecipeComponent,
    RegistrationComponent,
    LoginComponent,
    MyRecipesComponent,
    ChangeRecipeComponent,
    AddRecipeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    HttpClientModule,
    FontAwesomeModule,
    BrowserAnimationsModule,
    MatStepperModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MatInputModule
  ],
  providers: [RecipeService,
  ShareService,
  CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
