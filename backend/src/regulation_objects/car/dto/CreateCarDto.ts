import { IsString, IsNotEmpty, IsNumber } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class CreateCarDto {
  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  modelName: string;

  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  position: string;

  @IsNumber()
  @IsNotEmpty()
  @ApiProperty()
  speed: number;

  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  movementDirection: string;
}
