export * from './master.service';
import { MasterService } from './master.service';
export * from './register.service';
import { RegisterService } from './register.service';
export * from './test.service';
import { TestService } from './test.service';
export const APIS = [MasterService, RegisterService, TestService];
