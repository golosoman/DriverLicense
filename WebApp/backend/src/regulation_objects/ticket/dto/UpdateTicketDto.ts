import { IsString, IsNotEmpty, IsOptional, IsEnum, IsNumber } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';
import { TicketType } from './TicketType';

export class UpdateTicketDto {
  @IsString()
  @IsOptional()
  @ApiProperty()
  typeIntersection: TicketType;

  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  title: string;

  @IsString()
  @IsOptional()
  @ApiProperty()
  question: string;

  @IsString()
  @IsOptional()
  @ApiProperty()
  correctAnswer: string;
}
