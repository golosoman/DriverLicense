import {
    Controller,
    Get,
    Post,
    Body,
    Param,
    Put,
    Delete,
  } from '@nestjs/common';
  import { SignService } from './sign.service';
  import { CreateSignDto, UpdateSignDto } from './dto';
  
  @Controller('signs')
  export class SignController {
    constructor(private readonly signService: SignService) {}
  
    @Post()
    create(@Body() createSignDto: CreateSignDto) {
      return this.signService.create(createSignDto);
    }
  
    @Get()
    findAll() {
      return this.signService.findAll();
    }
  
    @Get(':id')
    findOne(@Param('id') id: string) {
      return this.signService.findOne(+id);
    }
  
    @Put(':id')
    update(@Param('id') id: string, @Body() updateSignDto: UpdateSignDto) {
      return this.signService.update(+id, updateSignDto);
    }
  
    @Delete(':id')
    remove(@Param('id') id: string) {
      return this.signService.remove(+id);
    }
  }
  