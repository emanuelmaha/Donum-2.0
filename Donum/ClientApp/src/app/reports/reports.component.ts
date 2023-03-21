import {Component, Inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Donations} from "../../models/donations";
import {MatDatepicker} from "@angular/material/datepicker";

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent {
  selectedDate: Date = new Date();

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  runReport() {
    this.http.get<string>(this.baseUrl + 'reports/allMembers?date='+this.selectedDate.toISOString()).subscribe(result => {
      alert(result);
    }, error => console.error(error));
  }
}

