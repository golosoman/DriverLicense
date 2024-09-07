import { IsString, IsNotEmpty, IsOptional, IsEnum, IsNumber } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';
import { TicketType } from './TicketType';

export class UpdateTicketDto {
  @IsString()
  @IsOptional()
  @ApiProperty()
  type: TicketType;

  @IsString()
  @IsOptional()
  @ApiProperty()
  question: string;

  @IsString()
  @IsOptional()
  @ApiProperty()
  correctAnswer: string;
}
