import { BaseModel } from '../../../_metronic/shared/crud-table';

export interface Country extends BaseModel {
  id: number;
  enName: string;
  arName: string;
  ISO2: string;
  status: number; // Active = 1 | Suspended = 2 | Pending = 3
}
