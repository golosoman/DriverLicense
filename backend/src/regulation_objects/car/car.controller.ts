import {
    Controller,
    Get,
    Post,
    Body,
    Param,
    Put,
    Delete,
  } from '@nestjs/common';
  import { CarService } from './car.service';
  import { CreateCarDto, UpdateCarDto } from './dto';
  
  @Controller('cars')
  export class CarController {
    constructor(private readonly carService: CarService) {}
  
    @Post()
    create(@Body() createCarDto: CreateCarDto) {
      return this.carService.create(createCarDto);
    }
  
    @Get()
    findAll() {
      return this.carService.findAll();
    }
  
    @Get(':id')
    findOne(@Param('id') id: string) {
      return this.carService.findOne(+id);
    }
  
    @Put(':id')
    update(@Param('id') id: string, @Body() updateCarDto: UpdateCarDto) {
      return this.carService.update(+id, updateCarDto);
    }
  
    @Delete(':id')
    remove(@Param('id') id: string) {
      return this.carService.remove(+id);
    }
  }
  