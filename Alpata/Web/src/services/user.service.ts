import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { LoginRequestDto } from 'src/Models/LoginRequestModel';
import { LoginResponseDto } from 'src/Models/LoginResponseDto';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = 'https://localhost:7143/user/';

  constructor(private http: HttpClient) {}

  login(model: LoginRequestDto) : Observable<LoginResponseDto> {
    return this.http.post(this.baseUrl + 'login', model) as Observable<LoginResponseDto>;
  }

  register(model: FormData) : Observable<LoginResponseDto> {
    return this.http.post(this.baseUrl + 'save', model) as Observable<LoginResponseDto>;
  }
}
