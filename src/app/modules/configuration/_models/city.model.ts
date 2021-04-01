import { BaseModel } from '../../../_metronic/shared/crud-table';

export interface City extends BaseModel {
  id: number;
  enName: string;
  arName: string;
  country_Id: number;
  status: number; // Active = 1 | Suspended = 2 | Pending = 3
}
