import { Module } from '@nestjs/common';
import { CreditApplicationController } from './credit-application.controller';
import { CreditApplicationService } from './credit-application.service';
import { WorkflowModule } from '../workflow/workflow.module';
import { CreditAnalysisModule } from '../credit-analysis/credit-analysis.module';

@Module({
  imports: [WorkflowModule, CreditAnalysisModule],
  controllers: [CreditApplicationController],
  providers: [CreditApplicationService],
  exports: [CreditApplicationService],
})
export class CreditApplicationModule {}
