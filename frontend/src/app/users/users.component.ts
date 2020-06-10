import {Component, OnInit, ViewChild} from '@angular/core';
import {User} from "../models/User";
import {MatSort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {RecipeService} from "../services/recipeService";
import {CookieService} from "ngx-cookie-service";
import {Router} from "@angular/router";
import {MatTableDataSource} from "@angular/material/table";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  users: User[] = [];
  datasource;
  displayedColumns: string[];

  constructor(private http: RecipeService, private cookie: CookieService, private router: Router) { }

  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  ngOnInit() {
    console.log(this.cookie.get('role'));
    if (this.cookie.get('role') != 'admin') {
      this.router.navigate(['/']);
    } else {
      this.http.getAllUsers(this.cookie.get('access_token')).subscribe(data => {
          this.datasource.data = data.body;
          console.log(this.datasource.data);
        },
        error => console.log(error));
      this.displayedColumns = ['login', 'email', 'role', 'actions'];
      this.datasource = new MatTableDataSource<User>();
      this.datasource.sort = this.sort;
      this.datasource.paginator = this.paginator;
    }
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.datasource.filter = filterValue.trim().toLowerCase();
  }

  blockUser(login) {
    this.http.blockUser(login, this.cookie.get('access_token')).subscribe( data => {
      console.log(data.status);
      this.http.getAllUsers(this.cookie.get('access_token')).subscribe(data => {
          this.datasource.data = data.body;
          console.log(this.datasource.data);
        },
        error => console.log(error));
    });
  }

  unBlockUser(login) {
    this.http.unBlockUser(login, this.cookie.get('access_token')).subscribe( data => {
      console.log(data.status);
      this.http.getAllUsers(this.cookie.get('access_token')).subscribe(data => {
          this.datasource.data = data.body;
          console.log(this.datasource.data);
        },
        error => console.log(error));
    });
  }

}
