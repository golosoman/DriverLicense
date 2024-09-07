import { IsString, IsNotEmpty, IsOptional, IsEnum, IsNumber, IsArray } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';
import { TicketType } from './TicketType';

export class CreateTicketDto {
  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  type: TicketType;

  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  question: string;

  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  correctAnswer: string;

  @IsArray()
  @ApiProperty()
  cars: number[];

  @IsArray()
  @ApiProperty()
  signs: number[];

  @IsArray()
  @ApiProperty()
  trafficLights: number[];
}
