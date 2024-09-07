import { Module } from '@nestjs/common';
import { TrafficLightController } from './traffic_light.controller';
import { TrafficLightService } from './traffic_light.service';
import { TrafficLight } from './traffic_light.model';
import { SequelizeModule } from '@nestjs/sequelize';

@Module({
  imports: [SequelizeModule.forFeature([TrafficLight])],
  controllers: [TrafficLightController],
  providers: [TrafficLightService],
})
export class TrafficLightModule {}
