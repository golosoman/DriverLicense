import { IsString, IsNotEmpty, IsOptional } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class UpdateTrafficLightDto {
  @IsString()
  @IsOptional()
  @ApiProperty()
  modelName: string;

  @IsString()
  @IsOptional()
  @ApiProperty()
  position: string;

  @IsString()
  @IsOptional()
  @ApiProperty()
  cycle: string;
}