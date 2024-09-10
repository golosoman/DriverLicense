import {
  Controller,
  Get,
  Post,
  Body,
  Param,
  Put,
  Delete,
  Query
} from '@nestjs/common';
import { TicketService } from './ticket.service';
import { CreateTicketDto, UpdateTicketDto, GetQuestionsDto } from './dto';

@Controller('tickets')
export class TicketController {
  constructor(private readonly ticketService: TicketService) {}

  @Post()
  create(@Body() createTicketDto: CreateTicketDto) {
    return this.ticketService.create(createTicketDto);
  }

  @Get()
  findAll() {
    return this.ticketService.findAll();
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    return this.ticketService.findOne(+id);
  }

  @Put(':id')
  update(@Param('id') id: string, @Body() updateTicketDto: UpdateTicketDto) {
    return this.ticketService.update(+id, updateTicketDto);
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    return this.ticketService.remove(+id);
  }

  @Get('test')
  getTest() {
    return this.ticketService.test();
  }


  @Get('questions')
  findQuestions(@Query('offset') offset: number, @Query('limit') limit: number) {
    const pageOffset = offset ? Number(offset) : 0;
    const pageLimit = limit ? Number(limit) : 10; // Значение по умолчанию — 10 вопросов
    return this.ticketService.findQuestions(pageOffset, pageLimit);
  }

}
