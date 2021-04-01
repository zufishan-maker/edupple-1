import { BaseModel } from '../../../_metronic/shared/crud-table';

export interface Tutor extends BaseModel {
  id: number;
  fullName: string;
  universityName: string;
  email: string;
  phoneNumber: string;
  gender: string;
  country: number;
  city: number,
  hourlyRate: string;
  description: string;
  subjects: any[],
  status: number; // Active = 1 | Suspended = 2 | Pending = 3
  dateOfBbirth: string;
  dob: Date;
}
