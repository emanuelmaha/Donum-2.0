import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html'
})
export class MembersComponent {
  public members: Member[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Member[]>(baseUrl + 'members').subscribe(result => {
      this.members = result;
    }, error => console.error(error));
  }
}

interface Member {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
