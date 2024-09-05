import { IsString, IsNotEmpty, IsOptional, IsNumber } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class CreateCarDto {
  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  sprite: string;

  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  direction: string;

  @IsNumber()
  @IsNotEmpty()
  @ApiProperty()
  speed: number;

  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  movementPath: string;
}
