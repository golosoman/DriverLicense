import {
    Controller,
    Get,
    Post,
    Body,
    Param,
    Put,
    Delete,
  } from '@nestjs/common';
  import { TrafficLightService } from './traffic_light.service';
  import { CreateTrafficLightDto, UpdateTrafficLightDto } from './dto';
  
  @Controller('traffic_lights')
  export class TrafficLightController {
    constructor(private readonly trafficLightService: TrafficLightService) {}
  
    @Post()
    create(@Body() createTrafficLightDto: CreateTrafficLightDto) {
      return this.trafficLightService.create(createTrafficLightDto);
    }
  
    @Get()
    findAll() {
      return this.trafficLightService.findAll();
    }
  
    @Get(':id')
    findOne(@Param('id') id: string) {
      return this.trafficLightService.findOne(+id);
    }
  
    @Put(':id')
    update(@Param('id') id: string, @Body() updateTrafficLightDto: UpdateTrafficLightDto) {
      return this.trafficLightService.update(+id, updateTrafficLightDto);
    }
  
    @Delete(':id')
    remove(@Param('id') id: string) {
      return this.trafficLightService.remove(+id);
    }
  }
  