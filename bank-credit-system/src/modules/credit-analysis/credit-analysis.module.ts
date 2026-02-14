import { Module } from '@nestjs/common';
import { CreditAnalysisService } from './credit-analysis.service';
import { CreditAnalysisController } from './credit-analysis.controller';
import { WorkflowModule } from '../workflow/workflow.module';

@Module({
  imports: [WorkflowModule],
  controllers: [CreditAnalysisController],
  providers: [CreditAnalysisService],
  exports: [CreditAnalysisService],
})
export class CreditAnalysisModule {}
