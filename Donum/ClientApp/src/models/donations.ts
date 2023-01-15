import {Member} from "./member";

export interface Donations {
  member: Member;
  sum: number;
  scope: string;
  dateOfReceived: string;
}
