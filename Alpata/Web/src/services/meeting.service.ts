import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Meeting } from 'src/Models/Meeting';

@Injectable({
  providedIn: 'root',
})
export class MeetingService {
  private baseUrl = 'https://localhost:7143/meeting/';

  constructor(private http: HttpClient) {}

  create(model: FormData) {
    return this.http.post(this.baseUrl + 'create', model);
  }

  getAll(userId: number) {
    return this.http.get(this.baseUrl + 'getAll/' + userId);
  }

  update(meeting: FormData) {
    return this.http.post(this.baseUrl + 'update', meeting);
  }

  delete(meetingId: number) {
    return this.http.delete(this.baseUrl + 'delete/' + meetingId);
  }
}
