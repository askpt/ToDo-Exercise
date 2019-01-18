import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private userService: UserService,
    private authService: AuthService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  submit(): void {
    const username = this.loginForm.value.username;
    const password = this.loginForm.value.password;
    this.userService.authenticate(username, password).subscribe(t => {
      this.authService.storeToken(t);
      this.router.navigate(['todo-list']);
    });
  }
}
