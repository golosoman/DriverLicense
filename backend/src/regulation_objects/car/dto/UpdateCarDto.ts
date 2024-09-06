import { IsString, IsOptional, IsNumber } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class UpdateCarDto {
  @IsString()
  @IsOptional()
  @ApiProperty()
  sprite: string;

  @IsString()
  @IsOptional()
  @ApiProperty()
  direction: string;

  @IsNumber()
  @IsOptional()
  @ApiProperty()
  speed: number;

  @IsString()
  @IsOptional()
  @ApiProperty()
  movementPath: string;
}
