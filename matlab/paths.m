%when installing cvx for the first time in a new environment run cvx/cvx_setup.m
%this will compile mex files and run tests to make sure it is working properly
%cvx_setup will also permanently add the cvx directory to matlab paths
%in order to maintain multiple cvx versions on one machine remove these paths
%then update the below cvx paths line to manually include the cvx path for this session

%cvx_path = ['..' filesep '..' filesep 'cvx'];
cvx_path = ['shared' filesep 'cvx'];

addpath(fullfile(fileparts(which(mfilename)), cvx_path));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'functions', 'square_'));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'functions', 'vec_'));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'structures'));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'lib'));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'functions'));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'commands'));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'builtins'));

addpath(fullfile(fileparts(which(mfilename)),'shared', 'kernels'));

%use genpath to include all folders in a directory
%addpath(genpath(fullfile(fileparts(which(mfilename)),'shared')));
