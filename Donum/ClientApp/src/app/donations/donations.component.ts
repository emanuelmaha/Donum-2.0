import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls:['./members.component.css']
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
  firstName: string;
  lastName: number;
  address: number;
}
