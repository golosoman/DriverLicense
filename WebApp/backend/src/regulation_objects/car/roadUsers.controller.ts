import {
  Controller,
  Get,
  Post,
  Body,
  Param,
  Put,
  Delete,
} from '@nestjs/common';
import { RoadUserService } from './roadUsers.service';
import { CreateRoadUserDto, UpdateRoadUserDto } from './dto';

@Controller('road-users')
export class RoadUserController {
  constructor(private readonly roadUserService: RoadUserService) {}

  @Post()
  create(@Body() createRoadUserDto: CreateRoadUserDto) {
    return this.roadUserService.create(createRoadUserDto);
  }

  @Get()
  findAll() {
    return this.roadUserService.findAll();
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    return this.roadUserService.findOne(+id);
  }

  @Put(':id')
  update(@Param('id') id: string, @Body() updateRoadUserDto: UpdateRoadUserDto) {
    return this.roadUserService.update(+id, updateRoadUserDto);
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    return this.roadUserService.remove(+id);
  }
}