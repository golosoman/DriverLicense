import { Module } from '@nestjs/common';
import { SignController } from './sign.controller';
import { SignService } from './sign.service';
import { Sign } from './sign.model';
import { SequelizeModule } from '@nestjs/sequelize';

@Module({
  imports: [SequelizeModule.forFeature([Sign])],
  controllers: [SignController],
  providers: [SignService],
})
export class SignModule {}
