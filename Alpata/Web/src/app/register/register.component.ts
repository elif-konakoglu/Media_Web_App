import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginResponseDto } from 'src/Models/LoginResponseDto';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  name: string;
  surname: string;
  email: string;
  phone: string;
  password: string;
  photo: File;

  constructor(private userService: UserService, private router: Router) {}

  onFileChange(event: any) {
    const fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      this.photo = fileList[0];
    }
  }

  register() {
    let formData: FormData = new FormData();
    formData.append('Name', this.name);
    formData.append('Surname', this.surname);
    formData.append('Email', this.email);
    formData.append('Phone', this.phone);
    formData.append('Password', this.password);
    formData.append('Photo', this.photo);

    this.userService.register(formData).subscribe(
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
