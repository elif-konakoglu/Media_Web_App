import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginRequestDto } from 'src/Models/LoginRequestModel';
import { LoginResponseDto } from 'src/Models/LoginResponseDto';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-login-component',
  templateUrl: './login.component.html',
  styleUrls: ['./login.css'],
})
export class LoginComponent {
  email: string;
  password: string;
  loginModel: LoginRequestDto;

  constructor(private userService: UserService, private router: Router) {}

  login() {
    this.loginModel = {
      email: this.email,
      password: this.password,
    };

    this.userService.login(this.loginModel).subscribe(
      (response: LoginResponseDto) => {
        localStorage.setItem('userId', response.id.toString());
        this.router.navigate(['/']);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
