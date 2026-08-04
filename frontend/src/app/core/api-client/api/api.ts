export * from './master.service';
import { MasterService } from './master.service';
export * from './register.service';
import { RegisterService } from './register.service';
export const APIS = [MasterService, RegisterService];
