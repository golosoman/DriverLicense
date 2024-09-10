import { Module } from '@nestjs/common';
import { RoadUserController } from './roadUsers.controller';
import { RoadUserService } from './roadUsers.service';
import { RoadUser } from './roadUsers.model';
import { SequelizeModule } from '@nestjs/sequelize';

@Module({
  imports: [SequelizeModule.forFeature([RoadUser])],
  controllers: [RoadUserController],
  providers: [RoadUserService],
})
export class CarModule {}
