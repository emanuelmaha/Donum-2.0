import {Member} from "../../models/member";

export interface Donations {
  member: Member;
  sum: number;
  scope: string;
  date: string;
}
