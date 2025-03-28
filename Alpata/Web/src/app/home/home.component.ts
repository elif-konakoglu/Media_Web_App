import { Component } from '@angular/core';
import { Meeting } from 'src/Models/Meeting';
import { MeetingService } from 'src/services/meeting.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  meetings: Meeting[] = [];
  currentMeeting: Meeting = new Meeting();
  userId: number;
  isEditing = false;

  constructor(private meetingService: MeetingService) {
    let localUserId = localStorage.getItem('userId');

    if (localUserId !== null && localUserId !== undefined) {
      this.userId = +localUserId;
      this.getAll(this.userId);
    }
  }

  onFileChange(event: any) {
    const fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      this.currentMeeting.document = fileList[0];
    }
  }

  getAll(userId: number) {
    this.meetingService.getAll(userId).subscribe(
      (response) => {
        this.meetings = response as Meeting[];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  editMeeting(meeting: Meeting) {
    this.isEditing = true;
    this.currentMeeting = { ...meeting };
  }

  deleteMeeting(meetingId: number) {
    this.meetingService.delete(meetingId).subscribe(
      () => {
        this.getAll(this.userId);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  saveMeeting() {
    let formData: FormData = new FormData();
    formData.append('UserId', this.userId.toString());
    formData.append('Name', this.currentMeeting.name);
    formData.append('Description', this.currentMeeting.description);
    formData.append('StartDate', this.currentMeeting.startDate.toString());
    formData.append('EndDate', this.currentMeeting.endDate.toString());
    formData.append('Document', this.currentMeeting.document);

    if (this.isEditing) {
      formData.append('Id', this.currentMeeting.id.toString());
      this.updateMeeting(formData);
      return;
    }

    this.createMeeting(formData);
  }

  updateMeeting(meeting: FormData) {
    this.meetingService.update(meeting).subscribe(
      () => {
        this.getAll(this.userId);
        this.resetForm();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  createMeeting(meeting: FormData) {
    this.meetingService.create(meeting).subscribe(
      () => {
        this.getAll(this.userId);
        this.resetForm();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  resetForm(): void {
    this.isEditing = false;
    this.currentMeeting = {
      id: 0,
      userId: 0,
      name: '',
      startDate: new Date(),
      endDate: new Date(),
      description: '',
      document: new File([], 'placeholder.txt', { type: 'text/plain' }),
    };
  }
}
