import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private userService: UserService) {
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
      localStorage.setItem('token', t);
      console.log(t);
      alert('Logged in');
    });
  }
}
