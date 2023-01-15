import {Component, Inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Donations} from "../../models/donations";

@Component({
  selector: 'app-donations',
  templateUrl: './donations.component.html',
  styleUrls: ['./donations.component.css']
})
export class DonationsComponent {
  public donations: Donations[] = [];
  public memberName: string = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.search();
  }

  search() {
    this.http.get<Donations[]>(this.baseUrl + 'donations?memberName=' + this.memberName).subscribe(result => {
      this.donations = result;
    }, error => console.error(error));
  }
}

