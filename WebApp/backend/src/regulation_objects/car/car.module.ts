import { Module } from '@nestjs/common';
import { CarController } from './car.controller';
import { CarService } from './car.service';
import { Car } from './car.model';
import { SequelizeModule } from '@nestjs/sequelize';

@Module({
  imports: [SequelizeModule.forFeature([Car])],
  controllers: [CarController],
  providers: [CarService],
})
export class CarModule {}
